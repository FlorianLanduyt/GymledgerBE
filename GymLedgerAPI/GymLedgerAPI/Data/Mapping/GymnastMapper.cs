using System;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymLedgerAPI.Data.Mapping
{
    public class GymnastMapper : IEntityTypeConfiguration<Gymnast>
    {
        public void Configure(EntityTypeBuilder<Gymnast> builder)
        {
            builder.ToTable("Gymnast");

            //builder.HasKey(g => g.GymnastId);

        }
    }
}
