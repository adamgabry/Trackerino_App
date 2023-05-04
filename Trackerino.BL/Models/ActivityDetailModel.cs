﻿using Trackerino.DAL.Common;

namespace Trackerino.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public required Guid ActivityId { get; set; }
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
        public ActivityTag Tag { get; set; }
        public string? Description { get; set; }
        public UserListModel? User { get; set; }
        public ProjectListModel? Project { get; set; }


        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ActivityId = Guid.Empty,
            StartDateTime = default(DateTime),
            EndDateTime = default(DateTime),
            Tag = ActivityTag.None,
        };
    }
}