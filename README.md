# JsonWebTokenExample
Before running, you will need to create a secret key and add the `JWT:Key` attribute to your user secrets.

Create secret key
```
openssl rand -hex 32
```
Add secret key to user secrets
```
dotnet user-secrets init
dotnet user-secrets set "JWT:Key" "SECRET_KEY"
```
