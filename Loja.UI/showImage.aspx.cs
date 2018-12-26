using System;

namespace Loja.UI.Pecadus
{
    public partial class showImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Read in the image filename to create a thumbnail of
                string image = Request.QueryString["img"];
                string imageHeight = Request.QueryString["h"];
                string imageWidth = Request.QueryString["w"];


                if (!String.IsNullOrEmpty(image))
                {
                    //Add on the appropriate directory
                    image = @"imagensProdutos/" + image;

                    //Get the image.    
                    System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath(image));

                    //Set the ContentType to "image/gif" and output the image's data
                    Response.ContentType = @"image/jpeg";

                    if (!String.IsNullOrEmpty(imageHeight) && !String.IsNullOrEmpty(imageWidth))
                    {
                        if (Convert.ToInt32(imageHeight) > 0 && Convert.ToInt32(imageWidth) > 0)
                        {
                            System.Drawing.Image thumbNailImg =
                                fullSizeImg.GetThumbnailImage(Convert.ToInt32(imageWidth), Convert.ToInt32(imageHeight), new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

                            thumbNailImg.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                            //Clean up / Dispose...
                            thumbNailImg.Dispose();
                        }
                    }
                    else
                    {
                        fullSizeImg.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    //Dispose/clean up...
                    fullSizeImg.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }
        protected bool ThumbnailCallback()
        {
            return true;
        }
    }
}