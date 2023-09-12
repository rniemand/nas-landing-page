<script lang="ts">
	import { ContainerClient, ContainerDto, ContainerItemDto } from '../../nlp-api';
	import Spinner from '../Spinner.svelte';
	import ItemCategoryInput from './ItemCategoryInput.svelte';
	import ItemSubCategoryInput from './ItemSubCategoryInput.svelte';

  export let container: ContainerDto | undefined = undefined;
  export let onItemAdded: () => void = () => {};

  let request: ContainerItemDto = new ContainerItemDto();
  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;
  let modalDialog: HTMLDialogElement;

  const addItem = async () => {
    saving = true;
    request.containerId = container?.containerId || 0;
    request.itemId = 0;
    request.orderMore = request.orderMore || false;
    request.orderMoreMinQty = request.orderMoreMinQty || 0;
    request.orderPlaced = request.orderPlaced || false;
    request.autoFlagOrderMore = request.autoFlagOrderMore || false;
    request.orderUrl = request.orderUrl || '';
    const response = await new ContainerClient().addContainerItem(request);
    saving = false;

    if(response.success) {
      onItemAdded();
      resetRequest();
      modalDialog.close();
      return;
    }
    
    message = response.error || '';
  };

  const syncRequest = () => {
    canAdd = false;
    if(request.category?.length < 2) return;
    if(request.subCategory?.length < 1) return;
    if(request.inventoryName?.length < 1) return;
    canAdd = true;
  };

  const resetRequest = () => {
    request = new ContainerItemDto({
      autoFlagOrderMore: false,
      category: '',
      containerId: container?.containerId || 0,
      inventoryName: '',
      itemId: 0,
      orderMore: false,
      orderMoreMinQty: 0,
      orderPlaced: false,
      orderUrl: '',
      quantity: 0,
      subCategory: '',
      containerLabel: '',
      containerName: '',
      itemCount: 0,
      notes: '',
      shelfLevel: 0,
      shelfNumber: 0,
      shelfRow: 0,
      shelfRowPosition: 0,
    });
    syncRequest();
  };
  
  const showModal = () => {
    resetRequest();
    canAdd = false;
    modalDialog.show();
  };
</script>

{#if container}
  <button class="btn btn-success" on:click={showModal}>Add Item</button>
{/if}

<dialog class="modal" bind:this={modalDialog}>
  <div class="modal-box">
    <h2 class="font-bold text-lg">Add Container Items ({container?.containerLabel})</h2>
    {#if saving}
    <Spinner />
    <div class="text-center mt-2">Adding item(s)...</div>
  {:else}
    <form>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label" for="quantity">
            <span class="label-text">Quantity</span>
          </label>
          <input type="number" class="input input-bordered w-full" min="0" id="quantity" bind:value={request.quantity} on:keyup={syncRequest} on:change={syncRequest}>
        </div>
        <div class="form-control flex-1">
          <label class="label" for="orderMoreQty">
            <span class="label-text">Order More Qty</span>
          </label>
          <input type="number" class="input input-bordered w-full" min="0" id="orderMoreQty" bind:value={request.orderMoreMinQty} on:keyup={syncRequest} on:change={syncRequest}>
        </div>
      </div>
      <div class="form-control">
        <label class="label" for="orderUrl">
          <span class="label-text">Order URL</span>
        </label>
        <input type="text" class="input input-bordered w-full" id="orderUrl" bind:value={request.orderUrl} on:keyup={syncRequest} on:change={syncRequest}>
      </div>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label" for="category">
            <span class="label-text">Category</span>
          </label>
          <ItemCategoryInput bind:value={request.category} />
        </div>
        <div class="form-control flex-1">
          <label class="label" for="subCategory">
            <span class="label-text">Sub subCategory</span>
          </label>
          <ItemSubCategoryInput bind:value={request.subCategory} bind:category={request.category} />
        </div>
      </div>      
      <div class="form-control">
        <label class="label" for="itemName">
          <span class="label-text">Item Name</span>
        </label>
        <input type="text" class="input input-bordered w-full" id="itemName" bind:value={request.inventoryName} on:keyup={syncRequest} on:change={syncRequest}>
      </div>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={request.orderMore} on:change={syncRequest} />
            <span class="label-text ml-2">Order More</span> 
          </label>
        </div>
        <div class="form-control flex-1 mr-3">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={request.orderPlaced} on:change={syncRequest} />
            <span class="label-text ml-2">Order Placed</span> 
          </label>
        </div>
        <div class="form-control flex-1">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={request.autoFlagOrderMore} on:change={syncRequest} />
            <span class="label-text ml-2">Auto-Order</span> 
          </label>
        </div>
      </div>
      {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
      <div class="col-12" style="text-align: right;">
        <button type="button" class="btn btn-primary w-full mt-2" disabled={!canAdd} on:click={addItem}>Add Item(s)</button>
      </div>
    </form>
  {/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>