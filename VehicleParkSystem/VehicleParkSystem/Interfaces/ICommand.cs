﻿using System.Collections.Generic;

// TODO: Documente esta contrato

namespace VehicleParkSystem.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        IDictionary<string, string> Parameters { get; }
    }
}

























