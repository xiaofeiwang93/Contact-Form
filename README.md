# Contact-Form
- Need to set up SMTP in the web.config between </system.data> and <system.web>
like this:
<system.net>
    <mailSettings>
      <smtp from="noreply@youremail.co.nz">
        <network host="smtp.sendgrid.net" userName="yoursmtpusername@email.com" password="yousmtppassword" />
      </smtp>
    </mailSettings>
  </system.net>
 
 - For Google reCAPTCHA, need to install RecaptchaNet (by tanveery) from the NuGet Package Manager. 
 
 - Also need to set up public key, private key and api version in appSettings
 like this:
 <appSettings>
    <add key="recaptchaPublicKey" value="yourrecaptchaPublickKey" />
    <add key="recaptchaPrivateKey" value="yourrecaptchaPrivateKey" />
    <add key="recaptchaApiVersion" value="2" />
 </appSettings>
 
 - In the page where you want to insert the form:
    @Html.Action("ActionName", "ControllerName") 
