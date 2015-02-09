using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using yops_lib.Constants;

namespace yops_lib.Classes
{
    /*
     * Custom class to set the header to application/json with text/html as fallback.
     */
    public class ApplicationJsonFormatter : JsonMediaTypeFormatter
    {
        public ApplicationJsonFormatter()
        {
            // add text/html as a supported format
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(SharedConstants.TEXT_HTML));

            // add UTF-8 and iso-8859-1 encoding
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding(SharedConstants.ISO_8859_1));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            // set the default content type to application/json
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue(SharedConstants.APPLICATION_JSON);
        }
    }
}
