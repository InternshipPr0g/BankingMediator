﻿using System.IO;
using System.Threading.Tasks;

namespace Internship.FileService.Service.Converters
{
    public interface IByteConvertable
    {
        StreamReader Convert(byte[] input);
    }
}