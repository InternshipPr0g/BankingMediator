﻿using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Internship.FileService.Domain.Models;
using Internship.FileService.Service.DBAccess;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Internship.FileService.Service.Consumers
{
    public class IncomingFileConsumer : IConsumer<IncomingFile>
    {
        private readonly ILogger<IncomingFileConsumer> _logger;
        private readonly HostBuilderContext _hostBuilderContext;
        private readonly InsertTransactionToDb _inserter;

        public IncomingFileConsumer(ILogger<IncomingFileConsumer> logger,
            HostBuilderContext hostBuilderContext, InsertTransactionToDb inserter)
        {
            _logger = logger;
            _hostBuilderContext = hostBuilderContext;
            _inserter = inserter;
        }

        public async Task Consume(ConsumeContext<IncomingFile> context)
        {
            _logger.LogInformation($"Received new message: {context.MessageId}, of type {context.GetType()}");

            const bool isIncomingTransaction = true;
            try
            {
                var configuration = _hostBuilderContext.Configuration;

                await _inserter.Insert(
                    configuration.GetConnectionString("MYSQLConnection"),
                    DateTime.Now, isIncomingTransaction,
                    context.Message.FileName,
                    context.Message.File);

                _logger.LogInformation($"File {context.Message.FileName} inserted successfully!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Inserting failed {context.MessageId}");
                _logger.LogDebug($"File size (bytes): {context.Message.File.Length.ToString()}");
                throw;
            }
        }
    }
}