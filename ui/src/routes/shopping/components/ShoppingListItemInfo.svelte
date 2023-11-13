<script lang="ts">
	import { Button, Icon } from 'sveltestrap';
	import type { ShoppingListItemDto } from '../../../nlp-api';

	export let item: ShoppingListItemDto;
	export let onEditItem: (item: ShoppingListItemDto) => void;
</script>

<div>here: {item.itemName}</div>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={() => onEditItem(item)}>
		<Icon name="pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<!-- <Button color="success" class="flex-fill" on:click={() => onCompleteTask(task)}>
		<Icon name="clipboard-check" />
		<span class="d-none d-sm-inline">Complete</span>
	</Button> -->
</div>
