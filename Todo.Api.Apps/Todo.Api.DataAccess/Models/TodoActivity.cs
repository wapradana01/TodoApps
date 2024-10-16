﻿using Todo.Api.DataAccess.Bases;
using Todo.Api.Shared.Enums;

namespace Todo.Api.DataAccess.Models
{
    public partial class TodoActivity : EntityBase
    {
        public string ActivityNo { get; set; } = string.Empty;
        public string ActivityTitle { get; set; } = string.Empty;
        public string ActivityDesc { get; set; } = string.Empty;
        public ActivityStatus ActivityStatus { get; set; }
    }
}
