<script lang="ts">
	import { ContainerClient, ContainerItemDto } from '../../nlp-api';
	import Spinner from '../Spinner.svelte';
	import ItemCategoryInput from './ItemCategoryInput.svelte';
	import ItemSubCategoryInput from './ItemSubCategoryInput.svelte';

  export let onItemSaved: () => void = () => {};
  export let item: ContainerItemDto = new ContainerItemDto();
  export const show = (_item: ContainerItemDto) => {
    item = _item;
    lastCategory = item.category;
    modalDialog.show();
  };

  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;
  let modalDialog: HTMLDialogElement;
  let lastCategory: string = '';

  const saveChanges = async () => {
    saving = true;
    const response = await new ContainerClient().updateContainerItem(item);
    saving = false;
    
    if(!response.success) {
      message = response.error || '';
      return;
    }

    onItemSaved();
    modalDialog.close();
  };

  const syncRequest = () => {
    canAdd = false;
    if(item.category?.length < 2) return;
    if(item.subCategory?.length < 1) return;
    if(item.inventoryName?.length < 1) return;
    canAdd = true;
  };

  const categoryChanged = (_cat: string) => {
    if(_cat === lastCategory) return;
    item.subCategory = '';
    lastCategory = _cat;
  };

  $: categoryChanged(item.category);
</script>

<dialog class="modal" bind:this={modalDialog}>
  <div class="modal-box">
    <h2 class="font-bold text-lg">Update Container Item</h2>
    {#if saving}
    <Spinner />
    <div class="text-center mt-2">Saving changes...</div>
  {:else}
    <form>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label" for="quantity">
            <span class="label-text">Quantity</span>
          </label>
          <input type="number" class="input input-bordered w-full" min="0" id="quantity" bind:value={item.quantity} on:keyup={syncRequest} on:change={syncRequest}>
        </div>
        <div class="form-control flex-1">
          <label class="label" for="orderMoreQty">
            <span class="label-text">Order More Qty</span>
          </label>
          <input type="number" class="input input-bordered w-full" min="0" id="orderMoreQty" bind:value={item.orderMoreMinQty} on:keyup={syncRequest} on:change={syncRequest}>
        </div>
      </div>
      <div class="form-control">
        <label class="label" for="orderUrl">
          <span class="label-text">Order URL</span>
        </label>
        <input type="text" class="input input-bordered w-full" id="orderUrl" bind:value={item.orderUrl} on:keyup={syncRequest} on:change={syncRequest}>
      </div>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label" for="category">
            <span class="label-text">Category</span>
          </label>
          <ItemCategoryInput bind:value={item.category} />
        </div>
        <div class="form-control flex-1">
          <label class="label" for="subCategory">
            <span class="label-text">Sub subCategory</span>
          </label>
          <ItemSubCategoryInput bind:value={item.subCategory} bind:category={item.category} />
        </div>
      </div>
      <div class="form-control">
        <label class="label" for="itemName">
          <span class="label-text">Item Name</span>
        </label>
        <input type="text" class="input input-bordered w-full" id="itemName" bind:value={item.inventoryName} on:keyup={syncRequest} on:change={syncRequest}>
      </div>
      <div class="flex">
        <div class="form-control flex-1 mr-3">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={item.orderMore} on:change={syncRequest} />
            <span class="label-text ml-2">Order More</span> 
          </label>
        </div>
        <div class="form-control flex-1 mr-3">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={item.orderPlaced} on:change={syncRequest} />
            <span class="label-text ml-2">Order Placed</span> 
          </label>
        </div>
        <div class="form-control flex-1">
          <label class="label cursor-pointer">
            <input type="checkbox" class="toggle" bind:checked={item.autoFlagOrderMore} on:change={syncRequest} />
            <span class="label-text ml-2">Auto-Order</span> 
          </label>
        </div>
      </div>
      {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
      <div class="col-12" style="text-align: right;">
        <button type="button" class="btn btn-primary w-full mt-2" disabled={!canAdd} on:click={saveChanges}>Save Changes</button>
      </div>
    </form>
  {/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>