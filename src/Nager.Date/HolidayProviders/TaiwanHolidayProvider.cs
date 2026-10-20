using Nager.Date.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Taiwan HolidayProvider
    /// </summary>
    internal sealed class TaiwanHolidayProvider : AbstractHolidayProvider
    {
        private static readonly ChineseLunisolarCalendar _chineseCalendar = new ChineseLunisolarCalendar();

        /// <summary>
        /// Taiwan HolidayProvider
        /// </summary>
        public TaiwanHolidayProvider() : base(CountryCode.TW)
        {
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            if (TaiwanHolidayCalendarData.TryGetHolidays(year, out var holidays))
            {
                return holidays;
            }

            return this.GetStatutoryHolidaySpecifications(year);
        }

        private IEnumerable<HolidaySpecification> GetStatutoryHolidaySpecifications(int year)
        {
            var holidaySpecifications = new List<HolidaySpecification>
            {
                Holiday("FOUNDINGDAY-01", year, 1, 1, "Founding Day of the Republic of China", "中華民國開國紀念日"),
                Holiday("PEACEMEMORIALDAY-01", year, 2, 28, "Peace Memorial Day", "和平紀念日"),
                Holiday("CHILDRENSDAY-01", year, 4, 4, "Children's Day", "兒童節"),
                Holiday("LABOURDAY-01", year, 5, 1, "Labour Day", "勞動節"),
                Holiday("NATIONALDAY-01", year, 10, 10, "National Day", "國慶日"),
            };

            if (year >= 2025)
            {
                holidaySpecifications.AddRange(
                [
                    Holiday("TEACHERSDAY-01", year, 9, 28, "Teachers' Day", "教師節"),
                    Holiday("TAIWANRETROCESSIONDAY-01", year, 10, 25, "Taiwan Retrocession Day and Memorial Day of Great Victory at Kuningtou Kinmen", "臺灣光復暨金門古寧頭大捷紀念日"),
                    Holiday("CONSTITUTIONDAY-01", year, 12, 25, "Constitution Day", "行憲紀念日"),
                ]);
            }

            if (year < _chineseCalendar.MinSupportedDateTime.Year || year > _chineseCalendar.MaxSupportedDateTime.Year)
            {
                return holidaySpecifications;
            }

            var leapMonth = _chineseCalendar.GetLeapMonth(year);
            var lunarNewYear = _chineseCalendar.ToDateTime(year, MoveMonth(1, leapMonth), 1, 0, 0, 0, 0);
            var dragonBoatFestival = _chineseCalendar.ToDateTime(year, MoveMonth(5, leapMonth), 5, 0, 0, 0, 0);
            var midAutumnFestival = _chineseCalendar.ToDateTime(year, MoveMonth(8, leapMonth), 15, 0, 0, 0, 0);
            var qingmingFestival = new DateTime(year, 4, leapMonth == 4 ? 5 : 4);

            holidaySpecifications.AddRange(
            [
                new HolidaySpecification
                {
                    Id = "QINGMINGFESTIVAL-01",
                    Date = qingmingFestival,
                    EnglishName = "Qingming Festival",
                    LocalName = "清明節",
                    HolidayTypes = HolidayTypes.Public,
                },
                Holiday("LUNARNEWYEAR-01", lunarNewYear.AddDays(-2), "Day before Lunar New Year's Eve", "小年夜"),
                Holiday("LUNARNEWYEAREVE-01", lunarNewYear.AddDays(-1), "Lunar New Year's Eve", "農曆除夕"),
                Holiday("LUNARNEWYEAR-02", lunarNewYear, "Lunar New Year", "春節"),
                Holiday("LUNARNEWYEAR-03", lunarNewYear.AddDays(1), "Second day of Lunar New Year", "春節"),
                Holiday("LUNARNEWYEAR-04", lunarNewYear.AddDays(2), "Third day of Lunar New Year", "春節"),
                Holiday("DRAGONBOATFESTIVAL-01", dragonBoatFestival, "Dragon Boat Festival", "端午節"),
                Holiday("MIDAUTUMNFESTIVAL-01", midAutumnFestival, "Mid-Autumn Festival", "中秋節"),
            ]);

            return holidaySpecifications;
        }

        private static HolidaySpecification Holiday(string id, int year, int month, int day, string englishName, string localName)
        {
            return Holiday(id, new DateTime(year, month, day), englishName, localName);
        }

        private static HolidaySpecification Holiday(string id, DateTime date, string englishName, string localName)
        {
            return new HolidaySpecification
            {
                Id = id,
                Date = date,
                EnglishName = englishName,
                LocalName = localName,
                HolidayTypes = HolidayTypes.Public,
            };
        }

        private static int MoveMonth(int month, int leapMonth)
        {
            if (leapMonth == 0 || leapMonth >= month)
            {
                return month;
            }

            return month + 1;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://data.gov.tw/dataset/14718",
                "https://law.moj.gov.tw/Eng/LawClass/LawAll.aspx?pcode=D0020095",
                "https://www.dgpa.gov.tw/information?uid=41&pid=12573",
            ];
        }
    }
}
