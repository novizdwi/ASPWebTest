namespace ASPWebTest.Models
{
    public class LoginOperation
    {
        public bool Succeeded { get; set; }
        public string? errors { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public static LoginOperation Success(int? accountId, int? userId, string? userName)
        {
            var result = new LoginOperation
            {
                Succeeded = true,
                AccountId = accountId,
                UserId = userId,
                UserName = userName,
            };

            return result;
        }
        public static LoginOperation Failed(string err = null)
        {
            var result = new LoginOperation { Succeeded = false };
            if (err != null)
            {
                result.errors = err;
            }
            return result;
        }

    }
    public class OperationResult
    {
        public bool Succeeded { get; set; }
        public string? errors { get; set; }

        public static OperationResult Success()
        {
            var result = new OperationResult { Succeeded = true };
            return result;
        }
        public static OperationResult Failed(string err = null)
        {
            var result = new OperationResult { Succeeded = false };
            if (err != null)
            {
                result.errors = err;
            }
            return result;
        }


    }
}
