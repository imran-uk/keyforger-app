#!/bin/bash

generate_payload()
{
	uuid=`curl --silent https://www.uuidgenerator.net/api/version4`
	echo ""
	cat <<EOF
  {
  	"id": "$uuid",
  	"deckName": "London4",
  	"houseList": [
    	0,1,3
  	],
  	"setName": 0
	}
EOF
}

add_keyforge_deck () {
	curl --insecure -X 'POST' \
  'https://localhost:7219/v1/Deck' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  --data "$(generate_payload)"
}

for request in {0..10}; do 
	echo "this is request number ${request}";
	add_keyforge_deck
done
