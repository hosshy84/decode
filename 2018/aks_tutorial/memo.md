git clone https://github.com/Azure-Samples/azure-voting-app-redis.git
cd azure-voting-app-redis/
docker-compose up -d
docker images
docker ps
docker-compose stop
docker-compose down

az group create --name myResourceGroup --location eastus
az acr create --resource-group myResourceGroup --name tatsuyaContainerRegistry --sku Basic
az acr login --name tatsuyaContainerRegistry
docker images
az acr list --resource-group myResourceGroup --query "[].{acrLoginServer:loginServer}" --output table
docker tag azure-vote-front tatsuyacontainerregistry.azurecr.io/azure-vote-front:v1
docker images
docker push tatsuyacontainerregistry.azurecr.io/azure-vote-front:v1
az acr repository list --name tatsuyaContainerRegistry --output table
az acr repository show-tags --name tatsuyaContainerRegistry --repository azure-vote-front --output table

az provider register -n Microsoft.ContainerService
az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --generate-ssh-keys
az aks install-cli
az aks get-credentials --resource-group myResourceGroup --name myAKSCluster
kubectl get nodes
CLIENT_ID=$(az aks show --resource-group myResourceGroup --name myAKSCluster --query "servicePrincipalProfile.clientId" --output tsv)
ACR_ID=$(az acr show --name tatsuyaContainerRegistry --resource-group myResourceGroup --query "id" --output tsv)
az role assignment create --assignee $CLIENT_ID --role Reader --scope $ACR_ID

az acr list --resource-group myResourceGroup --query "[].{acrLoginServer:loginServer}" --output table
kubectl create -f azure-vote-all-in-one-redis.yaml
kubectl get service azure-vote-front --watch
