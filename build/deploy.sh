echo "Start deploying ..."

echo "Start publishing .NET Core project ..."

dotnet publish ./src/Blog/Blog.csproj --no-restore --no-build --configuration Release

echo "Finished publishing .NET Core project ..."

echo "Start deploying .NET Core project ..."

scp -i "$DEPLOYER_SSH_PRIVATE_KEY" -r ./artifacts/bin/Release/Blog/netcoreapp2.2/publish $DEPLOYER_USERNAME@markdonile.com:/home/$DEPLOYER_USERNAME/markdonile.com

ssh -l $DEPLOYER_USERNAME -i "$DEPLOYER_SSH_PRIVATE_KEY" markdonile.com << HERE
    sudo systemctl stop kestrel.markdonile.com.service
    rm -r /var/www/markdonile.com/*
    cp -r /home/deployer/markdonile.com/. /var/www/markdonile.com/
    sudo systemctl start kestrel.markdonile.com.service
    logout
HERE

echo "Finished deploying .NET Core project ..."

echo "Finished deploying ..."