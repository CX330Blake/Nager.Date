using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class TaiwanTest
    {
        [TestMethod]
        public void Holidays2026_IncludeStatutoryAndLunisolarHolidays()
        {
            var holidays = HolidaySystem.GetHolidays(2026, CountryCode.TW).ToArray();

            Assert.IsTrue(holidays.Any(holiday => holiday.Date == new DateTime(2026, 2, 15) && holiday.EnglishName == "Day before Lunar New Year's Eve"));
            Assert.IsTrue(holidays.Any(holiday => holiday.Date == new DateTime(2026, 2, 20) && holiday.EnglishName == "Substitute Day for Lunar New Year"));
            Assert.IsTrue(holidays.Any(holiday => holiday.Date == new DateTime(2026, 6, 19) && holiday.EnglishName == "Dragon Boat Festival"));
            Assert.IsTrue(holidays.Any(holiday => holiday.Date == new DateTime(2026, 9, 25) && holiday.EnglishName == "Mid-Autumn Festival"));
            Assert.IsTrue(holidays.Any(holiday => holiday.Date == new DateTime(2026, 10, 10) && holiday.EnglishName == "National Day"));
        }

        [TestMethod]
        public void Holidays2027_AddSubstituteDaysForLunarNewYearWeekendHolidays()
        {
            var holidays = HolidaySystem.GetHolidays(2027, CountryCode.TW)
                .Where(holiday => holiday.EnglishName == "Substitute Day for Lunar New Year")
                .Select(holiday => holiday.Date)
                .ToArray();

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new DateTime(2027, 2, 9),
                    new DateTime(2027, 2, 10),
                },
                holidays);
        }
    }
}
