using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ContentTypeConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasKey(c => c.Key);
        builder.HasData(
            new Content { Key = "PROFILE_FULLNAME", Value = "Max Mustermann" },
            new Content { Key = "PROFILE_EMPLOYMENT", Value = "Applikationsentwickler" },
            new Content { Key = "ABOUT_TEXT", Value = "Hello!" },
            new Content { Key = "ABOUT_BIRTHDATE", Value = "25/10/2004" },
            new Content { Key = "ABOUT_EMAIL", Value = "max@ronniehartmann.ch" },
            new Content { Key = "ABOUT_EMAIL_LINK", Value = "mailto:max@ronniehartmann.ch" },
            new Content { Key = "ABOUT_PHONE", Value = "012 345 67 89" },
            new Content { Key = "ABOUT_PHONE_LINK", Value = "tel:0123456789" },
            new Content { Key = "ABOUT_GITHUB", Value = "ronniehartmann" },
            new Content { Key = "ABOUT_GITHUB_LINK", Value = "https://github.com/ronniehartmann" },
            new Content { Key = "ABOUT_RESIDENCE", Value = "9999 Musterstadt, Switzerland" }
        );
    }
}
