<script lang="ts">
	import { Button, Modal, ModalBody, ModalHeader } from 'sveltestrap';

	let open = false;

	const toggle = () => {
		open = !open;
	};
</script>

<Button color="success" on:click={toggle}>Add Chore</Button>
<Modal isOpen={open} {toggle}>
	<ModalHeader>Add Chore</ModalHeader>
	<ModalBody>Here</ModalBody>
</Modal>
