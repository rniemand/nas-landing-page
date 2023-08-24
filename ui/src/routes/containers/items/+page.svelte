<script lang="ts">
	import { page } from "$app/stores";
	import AddContainerItem from "../../../components/Containers/AddContainerItem.svelte";
	import UpdateContainerItem from "../../../components/Containers/UpdateContainerItem.svelte";
	import Spinner from "../../../components/Spinner.svelte";
	import { ContainerClient, ContainerDto, ContainerItemDto } from "../../../nlp-api";

  let container: ContainerDto | undefined = undefined;
  let items: ContainerItemDto[] = [];
  let loading: boolean = true;
  let _updateModal: UpdateContainerItem;

  const refreshContainerItems = async () => {
    if(!container) return;
    loading = true;
    items = await new ContainerClient().getContainerItems(container.containerId!) || [];
    loading = false;
  };
  
  const refreshContainerInfo = async (_id: number) => {
    if(_id === 0) return;
    container = undefined;
    container = await new ContainerClient().getContainer(_id);
    refreshContainerItems();
  };

  $: refreshContainerInfo(parseInt($page.url.searchParams.get('id') || '0'));
</script>

<div class="mb-3">
  <a href="/containers">Containers</a>
  &nbsp;
  <AddContainerItem {container} onItemAdded={refreshContainerItems} />
  <UpdateContainerItem bind:this={_updateModal} onItemSaved={refreshContainerItems} />
</div>

<Spinner show={!container || loading} />

{#if container}
  <table class="table table-striped table-hover table-bordered table-sm">
    <thead>
      <tr>
        <th scope="col">Qty</th>
        <th scope="col">Order</th>
        <th scope="col">Category</th>
        <th scope="col">SubCategory</th>
        <th scope="col">Name</th>
        <th scope="col">URL</th>
        <th scope="col">&nbsp;</th>
      </tr>
    </thead>
    <tbody>
      {#each items as item}
        <tr>
          <td>
            {item.quantity}
            <span>({item.orderMoreMinQty})</span>
            <span>({item.autoFlagOrderMore})</span>
          </td>
          <td>
            {item.orderMore}
            <span>({item.orderPlaced})</span>
          </td>
          <td>{item.category}</td>
          <td>{item.subCategory}</td>
          <td>{item.inventoryName}</td>
          <td>{item.orderUrl}</td>
          <td>
            <a href="#!" on:click={() => _updateModal.show(item)}>
              <i class="bi bi-pencil-square"></i>
            </a>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
{/if}
