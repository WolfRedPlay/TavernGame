using Core.Shared;
using Gameplay.Clients;
using UnityEngine;

namespace Gameplay.ClientsSystem
{
    internal class ClientsFactory
    {
        private ClientsFactory_Data _factoryData;



        public ClientsFactory(ClientsFactory_Data data) 
        { 
            _factoryData = data;
        }



        public CommonClient GetNewClient()
        {
            CommonClient newClient = GameObject.Instantiate(_factoryData.ClientPrefab).GetComponent<CommonClient>();

            AddClientHead(newClient);
            AddClientBody(newClient);
            AddClientBoots(newClient);
            AddClientEyes(newClient);

            return newClient;
        }


        private void AddClientHead(CommonClient client)
        {
            SkinnedMeshRenderer newHead = GameObject.Instantiate(_factoryData.HeadsData.HeadPrefabs.GetRandom(), client.transform);

            int materialIndex = -1;

            for (int i = 0; i < newHead.sharedMaterials.Length; i++)
            {
                if (newHead.sharedMaterials[i] == _factoryData.HeadsData.HairColorMaterial)
                {
                    materialIndex = i;
                    break;
                }
            }

            MaterialPropertyBlock block = new MaterialPropertyBlock();

            newHead.GetPropertyBlock(block, materialIndex);

            Color chosenColor = _factoryData.HeadsData.PossibleColors.GetRandom().Evaluate(Random.Range(0f,1f));

            block.SetColor("_BaseColor", chosenColor);

            newHead.SetPropertyBlock(block, materialIndex);

            newHead.rootBone = client.OriginalRenderer.rootBone;

            newHead.bones = client.OriginalRenderer.bones;
        }


        private void AddClientBody(CommonClient client)
        {
            BodyData chosenBody = _factoryData.BodiesData.Bodies.GetRandom();

            SkinnedMeshRenderer newBody = GameObject.Instantiate(chosenBody.Prefab, client.transform).GetComponent<SkinnedMeshRenderer>();

            int materialIndex = -1;

            for (int i = 0; i < newBody.sharedMaterials.Length; i++)
            {
                if (newBody.sharedMaterials[i] == _factoryData.BodiesData.BodyColorMaterial)
                {
                    materialIndex = i;
                    break;
                }
            }

            MaterialPropertyBlock block = new MaterialPropertyBlock();

            newBody.GetPropertyBlock(block, materialIndex);

            Color chosenColor = chosenBody.PossibleColors.GetRandom().Evaluate(Random.Range(0f, 1f));

            block.SetColor("_BaseColor", chosenColor);

            newBody.SetPropertyBlock(block, materialIndex);

            newBody.rootBone = client.OriginalRenderer.rootBone;

            newBody.bones = client.OriginalRenderer.bones;
        }


        private void AddClientEyes(CommonClient client)
        {
            SkinnedMeshRenderer newEyes = GameObject.Instantiate(_factoryData.EyesData.EyesPrefab, client.transform);

            int materialIndex = -1;

            for (int i = 0; i < newEyes.sharedMaterials.Length; i++)
            {
                if (newEyes.sharedMaterials[i] == _factoryData.EyesData.ColorMaterial)
                {
                    materialIndex = i;
                    break;
                }
            }



            MaterialPropertyBlock block = new MaterialPropertyBlock();

            newEyes.GetPropertyBlock(block, materialIndex);

            Color chosenColor = _factoryData.EyesData.PossibleColors.GetRandom().Evaluate(Random.Range(0f, 1f));

            block.SetColor("_BaseColor", chosenColor);

            newEyes.SetPropertyBlock(block, materialIndex);

            newEyes.rootBone = client.OriginalRenderer.rootBone;

            newEyes.bones = client.OriginalRenderer.bones;
        }


        private void AddClientBoots(CommonClient client)
        {
            SkinnedMeshRenderer newBoots = GameObject.Instantiate(_factoryData.BootsData.BootsPrefab, client.transform);

            MaterialPropertyBlock block = new MaterialPropertyBlock();

            newBoots.GetPropertyBlock(block);

            Color chosenColor = _factoryData.BootsData.PossibleColors.GetRandom().Evaluate(Random.Range(0f, 1f));

            block.SetColor("_BaseColor", chosenColor);

            newBoots.SetPropertyBlock(block);

            newBoots.rootBone = client.OriginalRenderer.rootBone;

            newBoots.bones = client.OriginalRenderer.bones;
        }
    }
}
