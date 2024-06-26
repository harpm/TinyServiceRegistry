﻿
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sardanapal.Domain;
using Sardanapal.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyServiceRegistry.Domain.Entities;

public class ServiceInstance : BaseEntityModel<int>
{
    [Required]
    public string IP { get; set; }
    [Required]
    public int Port { get; set; }
    public int ServiceId { get; set; }
    public bool IsActive { get; set; }
    public bool LastUse { get; set; }

    [ForeignKey(nameof(ServiceId))]
    public Service RelatedService { get; set; }
}

public class ServiceInstanceConfig : FluentModelConfig<ServiceInstance>
{
    public override void OnModelBuild(EntityTypeBuilder<ServiceInstance> entity)
    {
        entity.HasIndex(x => new { x.IP, x.Port })
            .IsUnique();
    }
}