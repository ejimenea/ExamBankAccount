namespace BankAccount.Utility
{
    public static class BankUtility
    {

        //Helper utility to compute the age of customer
        public static int CalculateAgeFromDOB(DateTime? paramDOB)
        {
            if (paramDOB == null) return 0;
            
            DateTime p_Dob = paramDOB.Value;
            DateTime Today = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(p_Dob).Ticks).Year - 1;
            DateTime PastYearDate = p_Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Today)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Today)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Today.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Today.Subtract(PastYearDate).Hours;
            int Minutes = Today.Subtract(PastYearDate).Minutes;
            int Seconds = Today.Subtract(PastYearDate).Seconds;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
            return Years;
        }

    }
}
