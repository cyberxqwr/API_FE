namespace Paslauga.Helpers
{
    public static class MappingHelpers
    {
        public static bool ShouldMap<T>(T srcMember)
        {
            if (srcMember == null)
                return false;

            if (srcMember is string str)
                return !string.IsNullOrWhiteSpace(str) && str != "string";

            if (srcMember is int i)
                return i != 0;

            return true;
        }
    }
}
 
