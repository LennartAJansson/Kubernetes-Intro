#!/bin/bash
set -x

for file in ./secrets/*; do
    filename=$(basename -- "$file")
    filename="${filename%.*}"
    
    key=$(echo $filename)

    secrets="${secrets} --from-file=$key=$file"
done

kubectl create secret generic buildversionsapi-secret --output json --dry-run=client ${secrets} | \
    C:/Apps/kubeseal/kubeseal \
    -n "buildversions" \
    --controller-namespace kube-system \
    --format yaml > "secret.yaml"