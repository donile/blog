echo "Start deploying ..."

echo "Start deploying .NET Core project ..."

# Create temp directory to hold SSH Private Key(s)
mkdir -p ./temp

DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH=./temp/DEPLOYER_SSH_PRIVATE_KEY

# Write SSH Private Key(s) to temporary file
cat > "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH" << END_TEXT
$DEPLOYER_SSH_PRIVATE_KEY
END_TEXT

# Change SSH Private Key File Permissions so it can be used by the 'scp' and
# 'ssh' commands that follow
chmod 600 "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH"

# Setup ~/.ssh directory
if [ ! -d "$HOME/.ssh" ]; then
  echo "$HOME/.ssh does not exists, creating it"
  mkdir -p "$HOME/.ssh"
fi

# Setup known_hosts file
if [ ! -f "$HOME/.ssh/known_hosts" ]; then
  echo "$HOME/.ssh/known_hosts does not exist, creating it"
  touch "$HOME/.ssh/known_hosts"
  chmod 644 "$HOME/.ssh/known_hosts"
fi

# Add production server's fingerprint to known_hosts file
ssh-keyscan markdonile.com >> ~/.ssh/known_hosts

# Clean directory on production server that temporarily holds app files
ssh -tt -l $DEPLOYER_USERNAME -i "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH" markdonile.com "rm -r /home/$DEPLOYER_USERNAME/markdonile.com"

# Create directory on production server to temporarily hold app files
ssh -tt -l $DEPLOYER_USERNAME -i "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH" markdonile.com "mkdir /home/$DEPLOYER_USERNAME/markdonile.com"

# Copy web app to temporary directory on production server
scp -i "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH" -r ./artifacts/bin/Release/Blog/netcoreapp2.2/publish/* $DEPLOYER_USERNAME@markdonile.com:/home/$DEPLOYER_USERNAME/markdonile.com

# Deploy web app on production server
ssh -tt -l $DEPLOYER_USERNAME -i "$DEPLOYER_SSH_PRIVATE_KEY_FILE_PATH" markdonile.com << HERE
    sudo systemctl stop kestrel.markdonile.com.service
    rm -r /var/www/markdonile.com/*
    cp -r /home/deployer/markdonile.com/. /var/www/markdonile.com/
    sudo systemctl start kestrel.markdonile.com.service
    logout
HERE

echo "Finished deploying .NET Core project ..."

echo "Start deleting ./temp directory and it's contents..."

rm -r ./temp

echo "Finished deleting ./temp directory and it's contents..."

echo "Finished deploying ..."