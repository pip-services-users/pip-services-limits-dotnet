using System.Collections.Generic;
using PipServicesLimitsDotnet.Data.Version1;

using PipServices.Commons.Commands;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Validate;

namespace PipServicesLimitsDotnet.Logic
{
    public class LimitsCommandSet : CommandSet
    {
        private ILimitsController _controller;

        public LimitsCommandSet(ILimitsController controller) 
        {
            _controller = controller;

            AddCommand(MakeGetLimitsCommand());
            AddCommand(MakeGetLimitByIdCommand());
            AddCommand(MakeGetLimitByUserIdCommand());

            AddCommand(MakeCreateLimitCommand());
            AddCommand(MakeUpdateLimitCommand());
            AddCommand(MakeDeleteLimitByIdCommand());

            AddCommand(MakeIncreaseLimitOfUserCommand());
            AddCommand(MakeDecreaseLimitOfUserCommand());
            AddCommand(MakeIncreaseAmountUsedByUserCommand());
            AddCommand(MakeDecreaseAmountUsedByUserCommand());
            AddCommand(MakeCanUserAddAmountCommand());
            AddCommand(MakeGetAmountAvailableToUserCommand());
        }

        private ICommand MakeGetLimitsCommand()
        {
            return new Command(
                "get_limits",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    return await _controller.GetLimitsAsync(correlationId, filter, paging);
                });
        }

        private ICommand MakeGetLimitByIdCommand()
        {
            return new Command(
                "get_limit_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("id");
                    return await _controller.GetLimitByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeGetLimitByUserIdCommand()
        {
            return new Command(
                "get_limit_by_user_id",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    return await _controller.GetLimitByUserIdAsync(correlationId, userId);
                });
        }


        private ICommand MakeCreateLimitCommand()
        {
            return new Command(
                "create_limit",
                new ObjectSchema()
                    .WithOptionalProperty("limit", new LimitV1Schema()),
                async (correlationId, parameters) =>
                {
                    var limit = ConvertToLimit(parameters.Get("limit"));
                    return await _controller.CreateLimitAsync(correlationId, limit);
                });
        }

        private ICommand MakeUpdateLimitCommand()
        {
            return new Command(
                "update_limit",
                new ObjectSchema()
                    .WithOptionalProperty("limit", new LimitV1Schema()),
                async (correlationId, parameters) =>
                {
                    var limit = ConvertToLimit(parameters.Get("limit"));
                    return await _controller.UpdateLimitAsync(correlationId, limit);
                });
        }

        private ICommand MakeDeleteLimitByIdCommand()
        {
            return new Command(
                "delete_limit_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("id");
                    return await _controller.DeleteLimitByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeIncreaseLimitOfUserCommand()
        {
            return new Command(
                "increase_limit_of_user",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String)
                    .WithRequiredProperty("increase_by", TypeCode.Long),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    var increaseBy = parameters.GetAsLong("increase_by");
                    return await _controller.IncreaseLimitOfUserAsync(correlationId, userId, increaseBy);
                });
        }

        private ICommand MakeDecreaseLimitOfUserCommand()
        {
            return new Command(
                "decrease_limit_of_user",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String)
                    .WithRequiredProperty("decrease_by", TypeCode.Long),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    var decreaseBy = parameters.GetAsLong("decrease_by");
                    return await _controller.DecreaseLimitOfUserAsync(correlationId, userId, decreaseBy);
                });
        }

        private ICommand MakeIncreaseAmountUsedByUserCommand()
        {
            return new Command(
                "increase_amount_used_by_user",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String)
                    .WithRequiredProperty("increase_by", TypeCode.Long),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    var increaseBy = parameters.GetAsLong("increase_by");
                    return await _controller.IncreaseAmountUsedByUserAsync(correlationId, userId, increaseBy);
                });
        }

        private ICommand MakeDecreaseAmountUsedByUserCommand()
        {
            return new Command(
                "decrease_amount_used_by_user",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String)
                    .WithRequiredProperty("decrease_by", TypeCode.Long),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    var decreaseBy = parameters.GetAsLong("decrease_by");
                    return await _controller.DecreaseAmountUsedByUserAsync(correlationId, userId, decreaseBy);
                });
        }

        private ICommand MakeCanUserAddAmountCommand()
        {
            return new Command(
                "can_user_add_amount",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String)
                    .WithRequiredProperty("amount", TypeCode.Long),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    var amount = parameters.GetAsLong("amount");
                    return await _controller.CanUserAddAmountAsync(correlationId, userId, amount);
                });
        }

        private ICommand MakeGetAmountAvailableToUserCommand()
        {
            return new Command(
                "get_amount_available_to_user",
                new ObjectSchema()
                    .WithRequiredProperty("user_id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var userId = parameters.GetAsString("user_id");
                    return await _controller.GetAmountAvailableToUserAsync(correlationId, userId);
                });
        }

        private static LimitV1 ConvertToLimit(object value)
        {
            return JsonConverter.FromJson<LimitV1>(JsonConverter.ToJson(value));
        }
    }
}