namespace Paslauga.Helpers
{
    public static class IPCalc
    {

        public static string CalculateIPs (string gatewayCIDR)
        {
            string[] parts = gatewayCIDR.Split('/');
            string[] decimals = parts[0].Split('.');
            return $"{decimals[0]}.{decimals[1]}.{decimals[2]}.";
        }

    }
}
 
