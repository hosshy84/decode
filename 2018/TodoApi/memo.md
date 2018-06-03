```
docker build -t hosshy84/testapp .
docker run -it --rm -p 5000:80 --name temp testapp
docker login
docker push hosshy84/testapp

kubectl config get-contexts
kubectl config use-context docker-for-desktop
kubectl get nodes
kubectl get po --all-namespaces

kubectl cluster-info
kubectl create -f https://raw.githubusercontent.com/kubernetes/dashboard/master/src/deploy/recommended/kubernetes-dashboard.yaml
kubectl proxy
http://127.0.0.1:8001/api/v1/namespaces/kube-system/services/https:kubernetes-dashboard:/proxy/#!/login
kubectl -n kube-system get secret
kubectl -n kube-system describe secret default-token-c9c7v

sudo xcodebuild -license accept
brew update && brew install azure-cli

az login
az provider register -n Microsoft.ContainerService
az group create --name myResourceGroup --location eastus
az acr create --resource-group myResourceGroup --name tatsuyaContainerRegistry --sku Basic
az acr login --name tatsuyaContainerRegistry
az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --generate-ssh-keys
az aks get-credentials --resource-group myResourceGroup --name myAKSCluster
kubectl get nodes

CLIENT_ID=$(az aks show --resource-group myResourceGroup --name myAKSCluster --query "servicePrincipalProfile.clientId" --output tsv)
ACR_ID=$(az acr show --name tatsuyaContainerRegistry --resource-group myResourceGroup --query "id" --output tsv)
az role assignment create --assignee $CLIENT_ID --role Reader --scope $ACR_ID

az acr list --resource-group myResourceGroup --query "[].{acrLoginServer:loginServer}" --output table
```