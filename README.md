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
  
 - Also need to set up public key, private key and api version in appSettings
 like this:
 <appSettings>
    <add key="recaptchaPublicKey" value="6LfTZkIUAAAAAFJhgq2IDbbhTjz7XXay4cJNWx8z" />
    <add key="recaptchaPrivateKey" value="6LfTZkIUAAAAAOvfGsZJHQ1aAzxeM4zRb_ScL8WT" />
    <add key="recaptchaApiVersion" value="2" />
 </appSettings>
