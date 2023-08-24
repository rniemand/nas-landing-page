<style>
  .buttons { font-size: 1.2em; }
  .info { font-size: 0.85em; color: rgb(1, 119, 1); }
  .small { font-size: 0.9em; }
  .cat { color: #282cdf; }
  .subCat { color: #f15555; }
</style>

<script lang="ts">
	import { page } from "$app/stores";
	import AddContainerItem from "../../../components/Containers/AddContainerItem.svelte";
	import UpdateContainerItem from "../../../components/Containers/UpdateContainerItem.svelte";
	import Spinner from "../../../components/Spinner.svelte";
	import VisualBool from "../../../components/VisualBool.svelte";
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
        <th scope="col">Categorization</th>
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
            {#if item.orderMoreMinQty > 0}
              <span class="info">({item.orderMoreMinQty})</span>
            {/if}
            <VisualBool value={item.autoFlagOrderMore} title="Auto Flag Order More" classTrue="bi bi-cart-check" hideIfFalse />
          </td>
          <td>
            <VisualBool value={item.orderMore} title="Order More" />
            <VisualBool value={item.orderPlaced} title="Order Placed" classTrue="bi bi-bag-check-fill" hideIfFalse />
          </td>
          <td class="small">
            <span class="cat">{item.category}</span>
            {#if item.subCategory}<span class="subCat">{item.subCategory}</span>{/if}
          </td>
          <td>{item.inventoryName}</td>
          <td>{item.orderUrl}</td>
          <td class="buttons">
            <a href="#!" on:click={() => _updateModal.show(item)}>
              <i class="bi bi-pencil-square"></i>
            </a>
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
{/if}
