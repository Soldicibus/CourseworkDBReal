﻿using CourseworkDB.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseworkDB.Data.Dto;

public class AdGroupsDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public string Audience { get; set; }
    public string? Description { get; set; }
    public float BidAmount { get; set; }
}
