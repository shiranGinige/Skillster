﻿namespace Skillster.Domain
{
    public interface INamedDocument
    {
        string Id { get; set; }
        string Name{ get; set; }
    }
}