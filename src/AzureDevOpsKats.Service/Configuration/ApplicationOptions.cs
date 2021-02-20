﻿using AzureDevOpsKats.Common.Configuration;

namespace AzureDevOpsKats.Service.Configuration
{
    public class ApplicationOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public FileStorage FileStorage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Dataprotection Dataprotection { get; set; }

        public MemoryHealthConfiguration MemoryHealthConfiguration { get; set; }
    }
}
