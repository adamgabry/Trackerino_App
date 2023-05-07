﻿using System.Collections.ObjectModel;

namespace Trackerino.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public ObservableCollection<UserProjectActivityListModel>? Activities { get; set; } = new();
        public ObservableCollection<UserProjectListModel> Projects { get; set; } = new();

        public static UserDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty,
        };
    }
}