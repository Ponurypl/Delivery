using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Attachments;

public sealed class AttachmentsEndpointGroup : Group
{
    public AttachmentsEndpointGroup()
    {
        Configure("Attachments", ep =>
                                 {
                                     ep.Description(d =>
                                                    {
                                                        d.ProducesValidationProblem();
                                                    });
                                     ep.AuthSchemes(AuthConsts.AccessSchema);

                                 });
    }
}