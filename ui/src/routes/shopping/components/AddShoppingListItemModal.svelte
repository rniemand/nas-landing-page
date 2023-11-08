<script lang="ts">
	import { Button, Modal, ModalHeader, ModalBody, Form, ModalFooter } from 'sveltestrap';

	let open = false;
	let canAdd: boolean = false;

	const toggle = () => {
		open = !open;
		// if (open) userTask = createNewTask();
	};

	const addShoppingListItem = () => {
		console.log('addShoppingListItem()');
	};
</script>

<div>
	<Button color="success" on:click={toggle}>Add Item</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Shopping List Item</ModalHeader>
		<ModalBody>
			<Form>Holder</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canAdd} on:click={addShoppingListItem}>Add Item</Button>
		</ModalFooter>
	</Modal>
</div>
