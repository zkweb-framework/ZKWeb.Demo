# How to enable https (use let's encrypt)

**install certbot**

see: https://certbot.eff.org/#ubuntuxenial-other

``` text
sudo apt-get update
sudo apt-get install software-properties-common
sudo add-apt-repository ppa:certbot/certbot
sudo apt-get update
sudo apt-get install certbot 
```

**generate cert**

``` text
certbot certonly --manual -d zkweb.org --config-dir . --work-dir . --logs-dir .
```

**update website**

Change IndexController.RequestHttps to return the text certbot required.
