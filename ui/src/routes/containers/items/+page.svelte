<script lang="ts">
	import { page } from "$app/stores";
	import AddContainerItem from "../../../components/Containers/AddContainerItem.svelte";
	import Spinner from "../../../components/Spinner.svelte";
	import { ContainerClient, ContainerDto } from "../../../nlp-api";

  let container: ContainerDto | undefined = undefined;
  
  const refreshContainerInfo = async (_id: number) => {
    if(_id === 0) return;
    
    container = undefined;
    container = await new ContainerClient().getContainer(_id);

    console.log('refreshing', container);
  };

  $: refreshContainerInfo(parseInt($page.url.searchParams.get('id') || '0'));
</script>

<Spinner show={!container} />

<div class="mb-3">
  <AddContainerItem {container} />
</div>

{#if container}
  Hello world
{/if}
