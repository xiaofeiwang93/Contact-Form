# Contact-Form
Need to set up SMTP in the web.config between </system.data> and <system.web>
like this:
<system.net>
    <mailSettings>
      <smtp from="noreply@youremail.co.nz">
        <network host="smtp.sendgrid.net" userName="yoursmtpusername@email.com" password="yousmtppassword" />
      </smtp>
    </mailSettings>
  </system.net>
