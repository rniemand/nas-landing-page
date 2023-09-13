<script lang="ts">
  import { ContainerClient, ContainerDto } from "../../nlp-api";
  import Spinner from "../Spinner.svelte";
  import AddContainer from "./modals/AddContainer.svelte";
  import EditContainer from "./modals/EditContainer.svelte";
  import ItemSearch from "./ItemSearch.svelte";
  import ContainerPagination from "./ContainerPagination.svelte";

  let containers: ContainerDto[] = [];
  let displayContainers: ContainerDto[] = [];
  let loading: boolean = false;
  let _editContainer: EditContainer;
  let searchTerm: string = '';

  const refreshContainers = async () => {
    loading = true;
    const response = await new ContainerClient().getAllContainers();
    containers = response || [];
    searchTerm = '';
    loading = false;
  };

  const matchesSearchTerm = (term: string, item: ContainerDto) => {
    if(item.containerLabel.toLowerCase().trim().indexOf(term) !== -1) return true;
    if(item.containerName.toLowerCase().trim().indexOf(term) !== -1) return true;
    return false;
  };

  const searchTermChanged = (term: string) => {
    if(term.length === 0) {
      // hack to trigger refresh of pagination
      containers = containers;
    } else {
      const safeTerm = term.toLowerCase().trim();
      displayContainers = containers.filter(x => matchesSearchTerm(safeTerm, x));
    }
  };

  const onPaginationChanged = (containers: ContainerDto[]) => displayContainers = containers;
  refreshContainers();

  $: searchTermChanged(searchTerm);
</script>

<div class="mb-3">
  <AddContainer onContainerAdded={refreshContainers} />
  <EditContainer bind:this={_editContainer} onContainerModified={refreshContainers} />
</div>

<div class="join join-vertical w-full mb-20">
  <div class="collapse collapse-arrow join-item border border-base-300">
    <input type="radio" name="my-accordion-4" checked /> 
    <div class="collapse-title text-xl font-medium bg-base-200">
      Containers
    </div>
    <div class="collapse-content">
      <Spinner show={loading} />
      {#if !loading}
        <ContainerPagination containers={containers} {onPaginationChanged} pageSize={15} bind:searchTerm={searchTerm} />
        <table class="table table-zebra">
          <thead>
            <tr>
              <th scope="col">Label</th>
              <th scope="col">Name</th>
              <th scope="col">Item Count</th>
              <!-- <th scope="col">Notes</th> -->
              <th scope="col">&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            {#each displayContainers as container}
              <tr class="hover">
                <td>{container.containerLabel}</td>
                <td>{container.containerName}</td>
                <td>{container.itemCount}</td>
                <!-- <td>{container.notes}</td> -->
                <td class="text-right">
                  <a href="/containers/items?id={container.containerId}" class="btn btn-sm btn-secondary">
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
    </div>
  </div>
  <div class="collapse collapse-arrow join-item border border-base-300">
    <input type="radio" name="my-accordion-4" /> 
    <div class="collapse-title text-xl font-medium bg-base-200">
      Search Container Items
    </div>
    <div class="collapse-content"> 
      <ItemSearch />
    </div>
  </div>
</div>