<script lang="ts">
  import { ContainerClient, ContainerDto } from "../../nlp-api";
	import Spinner from "../Spinner.svelte";
	import AddContainer from "./AddContainer.svelte";
	import EditContainer from "./EditContainer.svelte";
	import ItemSearch from "./ItemSearch.svelte";

  let containers: ContainerDto[] = [];
  let loading: boolean = false;
  let _editContainer: EditContainer;

  const refreshContainers = async () => {
    loading = true;
    const response = await new ContainerClient().getAllContainers();
    containers = response || [];
    loading = false;
  };
  
  refreshContainers();
</script>

<div class="mb-3">
  <AddContainer onContainerAdded={refreshContainers} />
  <EditContainer bind:this={_editContainer} onContainerModified={refreshContainers} />
</div>

<ItemSearch />

<Spinner show={loading} />
{#if !loading}
  <h1 class="text-2xl text-center">Containers</h1>
  <table class="table table-zebra mb-16">
    <thead>
      <tr>
        <th scope="col">Label</th>
        <th scope="col">Name</th>
        <th scope="col">Item Count</th>
        <th scope="col">Notes</th>
        <th scope="col">&nbsp;</th>
      </tr>
    </thead>
    <tbody>
      {#each containers as container}
        <tr class="hover">
          <td>{container.containerLabel}</td>
          <td>{container.containerName}</td>
          <td>{container.itemCount}</td>
          <td>{container.notes}</td>
          <td class="text-right">
            <a href="/containers/items?id={container.containerId}" class="btn btn-sm btn-primary">
              <i class="bi bi-binoculars-fill"></i>
            </a>
            <a href="#!" on:click={() => _editContainer.show(container)} class="btn btn-sm btn-primary">
              <i class="bi bi-pencil-square"></i>
            </a>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
{/if}