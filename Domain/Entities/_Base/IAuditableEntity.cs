﻿namespace Domain.Entities._Base
{
    public interface IAuditableEntity
    {
        public string? ActivityStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid EditBy { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
