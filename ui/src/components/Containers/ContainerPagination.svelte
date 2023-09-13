<script lang="ts">
  import type { ContainerDto } from "../../nlp-api";

  interface PageInfo {
    pageNumber: number;
    active: boolean;
  }

  export let containers: ContainerDto[] = [];
  export let pageSize: number = 10;
  export let searchTerm: string = '';
  export let onPaginationChanged: (containers: ContainerDto[]) => void;
  let showPaginator: boolean = false;
  let pages: PageInfo[] = [];

  const containersChanged = (_containers: ContainerDto[]) => {
    showPaginator = _containers.length > pageSize;
    pages = [];
    for(let i = 1; i < Math.ceil(_containers.length / pageSize)+1; i++) {
      pages.push({
        pageNumber: i,
        active: i === 1,
      });
    }
    setActivePage(1);
  };

  const setActivePage = (pageNumber: number) => {
    for(const page of pages) {
      page.active = pageNumber === page.pageNumber;
    }
    pages = pages;

    let startIdx = (pageNumber - 1) * pageSize;
    let endIdx = startIdx + pageSize;
    searchTerm = '';
    onPaginationChanged(containers.slice(startIdx, endIdx));
  };

  const clearSearch = () => searchTerm = '';

  $: containersChanged(containers);
</script>

<div class="flex my-3">
  <input type="text" placeholder="Search..." class="input input-bordered mr-1 flex-auto" bind:value={searchTerm} />
  <button type="button" class="btn btn-outline btn-warning mr-1" disabled={(searchTerm?.length || 0) === 0} on:click={clearSearch}>Clear</button>
  <div class="join mb-3">
    {#each pages as page}
      <button class="join-item btn" class:btn-active={page.active} on:click={() => setActivePage(page.pageNumber)}>
        {page.pageNumber}
      </button>
    {/each}
  </div>
</div>