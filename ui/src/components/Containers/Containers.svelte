<script lang="ts">
  import { ContainerClient, ContainerDto } from "../../nlp-api";
	import Spinner from "../Spinner.svelte";
	import AddContainer from "./AddContainer.svelte";
	import EditContainer from "./EditContainer.svelte";

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

<Spinner show={loading} />
{#if !loading}
  <table class="table table-striped table-hover table-bordered table-sm">
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
        <tr>
          <th>{container.containerLabel}</th>
          <td>{container.containerName}</td>
          <td>{container.itemCount}</td>
          <td>{container.notes}</td>
          <td>
            <a href="/containers/items?id={container.containerId}">
              <i class="bi bi-binoculars-fill"></i>
            </a>
            <a href="#!" on:click={() => _editContainer.show(container)}>
              <i class="bi bi-pencil-square"></i>
            </a>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
{/if}