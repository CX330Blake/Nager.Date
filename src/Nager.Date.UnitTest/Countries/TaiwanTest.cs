using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Nager.Date.UnitTest.Countries
{
    [TestClass]
    public class TaiwanTest
    {
        [TestMethod]
        public void Holidays2024_MatchGovernmentAdministrativeCalendar()
        {
            AssertHolidays(2024,
            [
                (1, 1), (2, 8), (2, 9), (2, 10), (2, 11), (2, 12), (2, 13), (2, 14), (2, 28),
                (4, 4), (4, 5), (6, 10), (9, 17), (10, 10),
            ]);
        }

        [TestMethod]
        public void Holidays2025_MatchGovernmentAdministrativeCalendar()
        {
            AssertHolidays(2025,
            [
                (1, 1), (1, 27), (1, 28), (1, 29), (1, 30), (1, 31), (2, 28), (4, 3), (4, 4),
                (5, 30), (5, 31), (9, 28), (9, 29), (10, 6), (10, 10), (10, 24), (10, 25), (12, 25),
            ]);
        }

        [TestMethod]
        public void Holidays2026_MatchGovernmentAdministrativeCalendar()
        {
            AssertHolidays(2026,
            [
                (1, 1), (2, 15), (2, 16), (2, 17), (2, 18), (2, 19), (2, 20), (2, 27), (2, 28),
                (4, 3), (4, 4), (4, 5), (4, 6), (5, 1), (6, 19), (9, 25), (9, 28), (10, 9), (10, 10),
                (10, 25), (10, 26), (12, 25),
            ]);
        }

        [TestMethod]
        public void Holidays2027_MatchGovernmentAdministrativeCalendar()
        {
            AssertHolidays(2027,
            [
                (1, 1), (2, 4), (2, 5), (2, 6), (2, 7), (2, 8), (2, 9), (2, 10), (2, 28), (3, 1),
                (4, 4), (4, 5), (4, 6), (4, 30), (5, 1), (6, 9), (9, 15), (9, 28), (10, 10), (10, 11),
                (10, 25), (12, 24), (12, 25), (12, 31),
            ]);
        }

        [TestMethod]
        public void Holidays2028_UseStatutoryFallbackWithoutGovernmentScheduleAdjustments()
        {
            var holidays = HolidaySystem.GetHolidays(2028, CountryCode.TW).ToArray();

            Assert.IsTrue(holidays.Any(holiday => holiday.EnglishName == "Lunar New Year"));
            Assert.IsFalse(holidays.Any(holiday => holiday.EnglishName == "Substitute Holiday"));
        }

        private static void AssertHolidays(int year, (int Month, int Day)[] expectedDates)
        {
            var actualDates = HolidaySystem.GetHolidays(year, CountryCode.TW)
                .Select(holiday => holiday.Date)
                .ToArray();
            var expected = expectedDates
                .Select(date => new DateTime(year, date.Month, date.Day))
                .ToArray();

            CollectionAssert.AreEquivalent(expected, actualDates);
        }
    }
}
