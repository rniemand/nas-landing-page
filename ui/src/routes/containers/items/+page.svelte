<style>
  .buttons { font-size: 1.2em; }
  .info { font-size: 0.85em; color: rgb(1, 119, 1); }
  .small { font-size: 0.9em; }
  .cat { color: #282cdf; }
  .subCat { color: #f15555; }
  .qty-btns button {
    margin-bottom: 2px;
    margin-right: 2px;
  }
  .qty-btns button:last-child { margin-right: 0; }
</style>

<script lang="ts">
	import { goto } from "$app/navigation";
	import { page } from "$app/stores";
	import AddContainerItem from "../../../components/Containers/AddContainerItem.svelte";
	import ContainerSelect from "../../../components/Containers/ContainerSelect.svelte";
	import UpdateContainerItem from "../../../components/Containers/UpdateContainerItem.svelte";
	import Spinner from "../../../components/Spinner.svelte";
	import VisualBool from "../../../components/VisualBool.svelte";
	import { ContainerClient, ContainerDto, ContainerItemDto } from "../../../nlp-api";

  let containerId: number = parseInt($page.url.searchParams.get('id') || '0');
  let container: ContainerDto | undefined = undefined;
  let items: ContainerItemDto[] = [];
  let loading: boolean = true;
  let runningQuery: boolean = false;
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

  const incrementQty = async (item: ContainerItemDto) => {
    runningQuery = true;
    const response = await new ContainerClient().incrementItemQuantity(item.itemId, 1);
    runningQuery = false;
    if(!response.success) {
      alert(response.error || 'Something went wrong!');
    } else {
      item.quantity += 1;
      items = items;
    }
  };

  const decrementQty = async (item: ContainerItemDto) => {
    runningQuery = true;
    const response = await new ContainerClient().decrementItemQuantity(item.itemId, 1);
    runningQuery = false;
    if(!response.success) {
      alert(response.error || 'Something went wrong!');
    } else {
      item.quantity -= 1;
      items = items;
    }
  };

  const setQty = async (item: ContainerItemDto) => {
    let value = prompt('Set new quantity', `${item.quantity}`);
    if(!value) return;
    const parsedInt = parseInt(value);
    if(isNaN(parsedInt)) {
      alert('You need to enter in a NUMBER here!');
      return;
    }
    runningQuery = true;
    const response = await new ContainerClient().setItemQuantity(item.itemId, parsedInt);
    runningQuery = false;
    if(!response.success) {
      alert(response.error || 'Something went wrong!');
    } else {
      item.quantity = parsedInt;
      items = items;
    }
  };

  const syncContainerId = (_id: number) => {
    if(parseInt($page.url.searchParams.get('id') || '0') === _id) return;
    $page.url.searchParams.set('id', `${_id}`);
    goto(`?${$page.url.searchParams.toString()}`);
  };

  $: syncContainerId(containerId);
  $: refreshContainerInfo(containerId);
</script>

<div class="mb-3">
  <ContainerSelect bind:value={containerId} />
  <AddContainerItem {container} onItemAdded={refreshContainerItems} />
  <UpdateContainerItem bind:this={_updateModal} onItemSaved={refreshContainerItems} />
</div>

<Spinner show={!container || loading} />

{#if container}
<div class="table-responsive">
  <table class="table table-striped table-hover table-bordered table-sm">
    <thead>
      <tr>
        <th scope="col">Name</th>
        <th scope="col" colspan="2">Qty</th>
        <th scope="col">Order</th>
        <th scope="col">Categorization</th>
        <th scope="col">URL</th>
        <th scope="col">&nbsp;</th>
      </tr>
    </thead>
    <tbody>
      {#each items as item}
        <tr>
          <td>{item.inventoryName}</td>
          <td class="qty-btns">
            <button class="btn btn-danger" disabled={runningQuery} on:click={() => decrementQty(item)}>-</button>
            <button class="btn btn-dark" disabled={runningQuery} on:click={() => setQty(item)}>set</button>
            <button class="btn btn-success" disabled={runningQuery} on:click={() => incrementQty(item)}>+</button>
          </td>
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
</div>
{/if}
