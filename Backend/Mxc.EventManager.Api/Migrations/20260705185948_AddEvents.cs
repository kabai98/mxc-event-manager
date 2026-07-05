using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mxc.EventManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT  INTO [EventManagerDb].[dbo].[Events]
                    ([Name],[Location],[Country],[Capacity])
                VALUES
                    ('Sziget Fesztivál', 'Budapest', 'Hungary', 95000),
                    ('Balaton Sound', 'Zamárdi', 'Hungary', 35000),
                    ('Oktoberfest', 'Munich', 'Germany', 6000000),
                    ('Tomorrowland', 'Boom', 'Belgium', 180000),
                    ('Glastonbury', 'Pilton', 'United Kingdom', 210000),
                    ('Coachella', 'Indio', 'United States', 250000),
                    ('Prague Jazz Castle', 'Prague', 'Czech Republic', 6500),
                    ('Copenhagen Eco Expo', 'Copenhagen', 'Denmark', 18000),
                    ('Tallinn Cyber Summit', 'Tallinn', 'Estonia', 3200),
                    ('Helsinki Ice Beats', 'Helsinki', 'Finland', 22000),
                    ('Paris Vineyard Days', 'Paris', 'France', 50000),
                    ('Lyon Gastro Nights', 'Lyon', 'France', 14000);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
                DELETE FROM [EventManagerDb].[dbo].[Events]
                WHERE [Name] IN (
                    'Sziget Fesztivál','Balaton Sound','Oktoberfest','Tomorrowland','Glastonbury', 
                    'Coachella','Prague Jazz Castle','Copenhagen Eco Expo','Tallinn Cyber Summit','Helsinki Ice Beats',
                    'Paris Vineyard Days','Lyon Gastro Nights'
                );
            ");
		}
    }
}
