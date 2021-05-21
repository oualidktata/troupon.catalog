namespace Troupon.Catalog.Core.Application.Behaviors
{
    //public class CustomRequestPostProcessorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;

    //    public CustomRequestPostProcessorBehavior(
    //        IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors)
    //    {
    //        this._postProcessors = postProcessors ?? throw new ArgumentNullException(nameof(postProcessors));
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        try
    //        {
    //            var response = await next().ConfigureAwait(false);

    //            foreach (var processor in this._postProcessors)
    //            {
    //                await processor.Process(request, response, cancellationToken).ConfigureAwait(false);
    //            }

    //            return response;
    //        }
    //        catch (Exception e)
    //        {
    //            // todo: log all exceptions

    //            throw;
    //        }
    //    }
    //}
}
