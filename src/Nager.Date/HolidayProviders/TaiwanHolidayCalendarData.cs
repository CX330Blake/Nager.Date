using Nager.Date.Models;
using System;
using System.Collections.Generic;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// Official Taiwan government administrative calendar data.
    /// </summary>
    internal static class TaiwanHolidayCalendarData
    {
        private static readonly IReadOnlyDictionary<int, HolidaySpecification[]> _holidays = new Dictionary<int, HolidaySpecification[]>
        {
            [2024] =
            [
                Holiday(20240101, "Founding Day of the Republic of China", "開國紀念日"),
                Holiday(20240208, "Day before Lunar New Year's Eve", "小年夜"),
                Holiday(20240209, "Lunar New Year's Eve", "農曆除夕"),
                Holiday(20240210, "Lunar New Year", "春節"),
                Holiday(20240211, "Second day of Lunar New Year", "春節"),
                Holiday(20240212, "Third day of Lunar New Year", "春節"),
                Holiday(20240213, "Substitute Holiday", "補假"),
                Holiday(20240214, "Substitute Holiday", "補假"),
                Holiday(20240228, "Peace Memorial Day", "和平紀念日"),
                Holiday(20240404, "Children's Day and Tomb Sweeping Day", "兒童節及民族掃墓節"),
                Holiday(20240405, "Substitute Holiday", "補假"),
                Holiday(20240610, "Dragon Boat Festival", "端午節"),
                Holiday(20240917, "Mid-Autumn Festival", "中秋節"),
                Holiday(20241010, "National Day", "國慶日"),
            ],
            [2025] =
            [
                Holiday(20250101, "Founding Day of the Republic of China", "開國紀念日"),
                Holiday(20250127, "Day before Lunar New Year's Eve", "小年夜"),
                Holiday(20250128, "Lunar New Year's Eve", "農曆除夕"),
                Holiday(20250129, "Lunar New Year", "春節"),
                Holiday(20250130, "Second day of Lunar New Year", "春節"),
                Holiday(20250131, "Third day of Lunar New Year", "春節"),
                Holiday(20250228, "Peace Memorial Day", "和平紀念日"),
                Holiday(20250403, "Substitute Holiday", "補假"),
                Holiday(20250404, "Children's Day and Tomb Sweeping Day", "兒童節及民族掃墓節"),
                Holiday(20250530, "Substitute Holiday", "補假"),
                Holiday(20250531, "Dragon Boat Festival", "端午節"),
                Holiday(20250928, "Confucius' Birthday", "孔子誕辰紀念日"),
                Holiday(20250929, "Substitute Holiday", "補假"),
                Holiday(20251006, "Mid-Autumn Festival", "中秋節"),
                Holiday(20251010, "National Day", "國慶日"),
                Holiday(20251024, "Substitute Holiday", "補假"),
                Holiday(20251025, "Taiwan Retrocession Day and Memorial Day of Great Victory at Kuningtou Kinmen", "臺灣光復暨金門古寧頭大捷紀念日"),
                Holiday(20251225, "Constitution Day", "行憲紀念日"),
            ],
            [2026] =
            [
                Holiday(20260101, "Founding Day of the Republic of China", "開國紀念日"),
                Holiday(20260215, "Day before Lunar New Year's Eve", "小年夜"),
                Holiday(20260216, "Lunar New Year's Eve", "農曆除夕"),
                Holiday(20260217, "Lunar New Year", "春節"),
                Holiday(20260218, "Second day of Lunar New Year", "春節"),
                Holiday(20260219, "Third day of Lunar New Year", "春節"),
                Holiday(20260220, "Substitute Holiday", "補假"),
                Holiday(20260227, "Substitute Holiday", "補假"),
                Holiday(20260228, "Peace Memorial Day", "和平紀念日"),
                Holiday(20260403, "Substitute Holiday", "補假"),
                Holiday(20260404, "Children's Day", "兒童節"),
                Holiday(20260405, "Qingming Festival", "清明節"),
                Holiday(20260406, "Substitute Holiday", "補假"),
                Holiday(20260501, "Labour Day", "勞動節"),
                Holiday(20260619, "Dragon Boat Festival", "端午節"),
                Holiday(20260925, "Mid-Autumn Festival", "中秋節"),
                Holiday(20260928, "Confucius' Birthday and Teachers' Day", "孔子誕辰紀念日/教師節"),
                Holiday(20261009, "Substitute Holiday", "補假"),
                Holiday(20261010, "National Day", "國慶日"),
                Holiday(20261025, "Taiwan Retrocession Day and Memorial Day of Great Victory at Kuningtou Kinmen", "臺灣光復暨金門古寧頭大捷紀念日"),
                Holiday(20261026, "Substitute Holiday", "補假"),
                Holiday(20261225, "Constitution Day", "行憲紀念日"),
            ],
            [2027] =
            [
                Holiday(20270101, "Founding Day of the Republic of China", "開國紀念日"),
                Holiday(20270204, "Day before Lunar New Year's Eve", "小年夜"),
                Holiday(20270205, "Lunar New Year's Eve", "農曆除夕"),
                Holiday(20270206, "Lunar New Year", "春節"),
                Holiday(20270207, "Second day of Lunar New Year", "春節"),
                Holiday(20270208, "Third day of Lunar New Year", "春節"),
                Holiday(20270209, "Substitute Holiday", "補假"),
                Holiday(20270210, "Substitute Holiday", "補假"),
                Holiday(20270228, "Peace Memorial Day", "和平紀念日"),
                Holiday(20270301, "Substitute Holiday", "補假"),
                Holiday(20270404, "Children's Day", "兒童節"),
                Holiday(20270405, "Qingming Festival", "清明節"),
                Holiday(20270406, "Substitute Holiday", "補假"),
                Holiday(20270430, "Substitute Holiday", "補假"),
                Holiday(20270501, "Labour Day", "勞動節"),
                Holiday(20270609, "Dragon Boat Festival", "端午節"),
                Holiday(20270915, "Mid-Autumn Festival", "中秋節"),
                Holiday(20270928, "Confucius' Birthday and Teachers' Day", "孔子誕辰紀念日/教師節"),
                Holiday(20271010, "National Day", "國慶日"),
                Holiday(20271011, "Substitute Holiday", "補假"),
                Holiday(20271025, "Taiwan Retrocession Day and Memorial Day of Great Victory at Kuningtou Kinmen", "臺灣光復暨金門古寧頭大捷紀念日"),
                Holiday(20271224, "Substitute Holiday", "補假"),
                Holiday(20271225, "Constitution Day", "行憲紀念日"),
                Holiday(20271231, "Substitute Holiday", "補假"),
            ],
        };

        internal static bool TryGetHolidays(int year, out HolidaySpecification[] holidays)
        {
            return _holidays.TryGetValue(year, out holidays!);
        }

        private static HolidaySpecification Holiday(int date, string englishName, string localName)
        {
            var year = date / 10000;
            var month = date / 100 % 100;
            var day = date % 100;

            return new HolidaySpecification
            {
                Id = $"CALENDAR-{date}",
                Date = new DateTime(year, month, day),
                EnglishName = englishName,
                LocalName = localName,
                HolidayTypes = HolidayTypes.Public,
            };
        }
    }
}
