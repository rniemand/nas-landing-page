<script lang="ts">
	import { Button, Icon } from 'sveltestrap';
	import type { ShoppingListItemDto } from '../../../nlp-api';

	export let item: ShoppingListItemDto;
	export let onEditItem: (item: ShoppingListItemDto) => void;
	export let onItemBought: (item: ShoppingListItemDto) => void;
	export let onDeleteItem: (item: ShoppingListItemDto) => void;

	const onDeleteConfirm = () => {
		if (!confirm('Are you dure?')) return;
		onDeleteItem(item);
	};
</script>

<div>here: {item.itemName}</div>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={() => onEditItem(item)}>
		<Icon name="pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button color="danger" class="flex-fill me-1" on:click={onDeleteConfirm}>
		<Icon name="trash" />
		<span class="d-none d-sm-inline">Remove</span>
	</Button>
	<Button color="success" class="flex-fill" on:click={() => onItemBought(item)}>
		<Icon name="clipboard-check" />
		<span class="d-none d-sm-inline">Bought</span>
	</Button>
</div>
