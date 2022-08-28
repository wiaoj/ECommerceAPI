using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceAPI.Infrastructure.Filters;
public class ValidationFilter : IAsyncActionFilter {
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
		if(context.ModelState.IsValid.Equals(default)) {
			var errors = context.ModelState.Where(x => x.Value.Errors.Any())
				.ToDictionary(e => e.Key, e => e.Value.Errors.Select(
					e => e.ErrorMessage
					)).ToArray();
			context.Result = new BadRequestObjectResult(errors);
			return; //diğer filtera geçmesini engelliyor hata var neden geçsin?
		}
		await next(); //sonraki filtera yönlendiriyor
	}
}