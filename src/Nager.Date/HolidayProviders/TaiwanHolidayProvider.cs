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
        private static readonly ObservedRuleSet _weekendObservedRuleSet = new ObservedRuleSet
        {
            Saturday = date => date.AddDays(-1),
            Sunday = date => date.AddDays(1),
        };

        /// <summary>
        /// Taiwan HolidayProvider
        /// </summary>
        public TaiwanHolidayProvider() : base(CountryCode.TW)
        {
        }

        /// <inheritdoc/>
        protected override IEnumerable<HolidaySpecification> GetHolidaySpecifications(int year)
        {
            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "FOUNDINGDAY-01",
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "Founding Day of the Republic of China",
                    LocalName = "中華民國開國紀念日",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "PEACEMEMORIALDAY-01",
                    Date = new DateTime(year, 2, 28),
                    EnglishName = "Peace Memorial Day",
                    LocalName = "和平紀念日",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "CHILDRENSDAY-01",
                    Date = new DateTime(year, 4, 4),
                    EnglishName = "Children's Day",
                    LocalName = "兒童節",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "QINGMINGFESTIVAL-01",
                    Date = this.GetQingmingFestival(year),
                    EnglishName = "Qingming Festival",
                    LocalName = "清明節",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "LABOURDAY-01",
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "勞動節",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "NATIONALDAY-01",
                    Date = new DateTime(year, 10, 10),
                    EnglishName = "National Day",
                    LocalName = "國慶日",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
            };

            if (year >= 2025)
            {
                holidaySpecifications.AddRange(
                [
                    new HolidaySpecification
                    {
                        Id = "TEACHERSDAY-01",
                        Date = new DateTime(year, 9, 28),
                        EnglishName = "Teachers' Day",
                        LocalName = "教師節",
                        HolidayTypes = HolidayTypes.Public,
                        ObservedRuleSet = _weekendObservedRuleSet,
                    },
                    new HolidaySpecification
                    {
                        Id = "TAIWANRETROCESSIONDAY-01",
                        Date = new DateTime(year, 10, 25),
                        EnglishName = "Taiwan Retrocession Day and Memorial Day of Great Victory at Kuningtou Kinmen",
                        LocalName = "臺灣光復暨金門古寧頭大捷紀念日",
                        HolidayTypes = HolidayTypes.Public,
                        ObservedRuleSet = _weekendObservedRuleSet,
                    },
                    new HolidaySpecification
                    {
                        Id = "CONSTITUTIONDAY-01",
                        Date = new DateTime(year, 12, 25),
                        EnglishName = "Constitution Day",
                        LocalName = "行憲紀念日",
                        HolidayTypes = HolidayTypes.Public,
                        ObservedRuleSet = _weekendObservedRuleSet,
                    },
                ]);
            }

            holidaySpecifications.AddRange(this.GetLunisolarHolidays(year));

            return holidaySpecifications;
        }

        private DateTime GetQingmingFestival(int year)
        {
            var centuryConstant = year <= 2000 ? 5.59 : 4.81;
            var yearOfCentury = year % 100;
            var day = (int)Math.Floor(yearOfCentury * 0.2422 + centuryConstant) - yearOfCentury / 4;

            if (year == 2008)
            {
                day--;
            }

            return new DateTime(year, 4, day);
        }

        private IEnumerable<HolidaySpecification> GetLunisolarHolidays(int year)
        {
            var chineseCalendar = new ChineseLunisolarCalendar();
            if (year < chineseCalendar.MinSupportedDateTime.Year || year > chineseCalendar.MaxSupportedDateTime.Year)
            {
                return [];
            }

            var leapMonth = chineseCalendar.GetLeapMonth(year);
            var lunarNewYear = chineseCalendar.ToDateTime(year, this.MoveMonth(1, leapMonth), 1, 0, 0, 0, 0);
            var dragonBoatFestival = chineseCalendar.ToDateTime(year, this.MoveMonth(5, leapMonth), 5, 0, 0, 0, 0);
            var midAutumnFestival = chineseCalendar.ToDateTime(year, this.MoveMonth(8, leapMonth), 15, 0, 0, 0, 0);
            var lunarNewYearHolidays = new[]
            {
                lunarNewYear.AddDays(-2),
                lunarNewYear.AddDays(-1),
                lunarNewYear,
                lunarNewYear.AddDays(1),
                lunarNewYear.AddDays(2),
            };

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-01",
                    Date = lunarNewYearHolidays[0],
                    EnglishName = "Day before Lunar New Year's Eve",
                    LocalName = "小年夜",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LUNARNEWYEAREVE-01",
                    Date = lunarNewYearHolidays[1],
                    EnglishName = "Lunar New Year's Eve",
                    LocalName = "農曆除夕",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-02",
                    Date = lunarNewYearHolidays[2],
                    EnglishName = "Lunar New Year",
                    LocalName = "春節",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-03",
                    Date = lunarNewYearHolidays[3],
                    EnglishName = "Second day of Lunar New Year",
                    LocalName = "春節",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "LUNARNEWYEAR-04",
                    Date = lunarNewYearHolidays[4],
                    EnglishName = "Third day of Lunar New Year",
                    LocalName = "春節",
                    HolidayTypes = HolidayTypes.Public,
                },
                new HolidaySpecification
                {
                    Id = "DRAGONBOATFESTIVAL-01",
                    Date = dragonBoatFestival,
                    EnglishName = "Dragon Boat Festival",
                    LocalName = "端午節",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
                new HolidaySpecification
                {
                    Id = "MIDAUTUMNFESTIVAL-01",
                    Date = midAutumnFestival,
                    EnglishName = "Mid-Autumn Festival",
                    LocalName = "中秋節",
                    HolidayTypes = HolidayTypes.Public,
                    ObservedRuleSet = _weekendObservedRuleSet,
                },
            };

            var substituteHoliday = lunarNewYear.AddDays(3);
            var substituteHolidayIndex = 1;
            foreach (var lunarNewYearHoliday in lunarNewYearHolidays)
            {
                if (lunarNewYearHoliday.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday))
                {
                    continue;
                }

                while (substituteHoliday.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                    substituteHoliday = substituteHoliday.AddDays(1);
                }

                holidaySpecifications.Add(new HolidaySpecification
                {
                    Id = $"LUNARNEWYEARSUBSTITUTE-{substituteHolidayIndex++:00}",
                    Date = substituteHoliday,
                    EnglishName = "Substitute Day for Lunar New Year",
                    LocalName = "春節補假",
                    HolidayTypes = HolidayTypes.Public,
                });

                substituteHoliday = substituteHoliday.AddDays(1);
            }

            return holidaySpecifications;
        }


        private int MoveMonth(int month, int leapMonth)
        {
            if (leapMonth == 0)
            {
                return month;
            }

            if (leapMonth < month)
            {
                return ++month;
            }

            return month;
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetSources()
        {
            return
            [
                "https://law.moj.gov.tw/Eng/LawClass/LawAll.aspx?pcode=D0020095",
                "https://www.dgpa.gov.tw/information?uid=41&pid=12573",
            ];
        }
    }
}
