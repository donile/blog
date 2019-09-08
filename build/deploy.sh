echo "Start deploying ..."

ssh -l $DEPLOYER_USERNAME -i "$DEPLOYER_SSH_PRIVATE_KEY" markdonile.com << HERE
    touch "/home/deployer/jenkins-pipeline-created-file"
    logout
HERE

echo "Start publishing .NET Core project ..."

# dotnet publish ./src/Blog/Blog.csproj --configuration Release

echo "Finished publishing .NET Core project ..."

echo "Start deploying .NET Core project ..."

echo "Finished deploying .NET Core projec ..."

echo "Finished deploying ..."