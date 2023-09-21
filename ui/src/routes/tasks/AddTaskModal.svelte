<script lang="ts">
	import {
		Button,
		Input,
		Form,
		FormGroup,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Label,
		Col,
		Row
	} from 'sveltestrap';
	import TaskPriority from './TaskPriority.svelte';
	import { createNewTask } from './Tasks';
	import type { UserTaskDto } from '../../nlp-api';

	let open = false;
	let userTask: UserTaskDto = createNewTask();

	const toggle = () => (open = !open);
</script>

<div>
	<Button color="success" on:click={toggle}>Add Task</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Task</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label for="name">Task Name</Label>
							<Input type="text" id="name" bind:value={userTask.taskName} />
						</FormGroup>
					</Col>
					<Col>
						<TaskPriority bind:value={userTask.taskPriority} />
					</Col>
				</Row>
				<Row>
					<Col>
						<FormGroup>
							<Label for="category">Category</Label>
							<Input type="text" id="category" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label for="subCategory">Sub Category</Label>
							<Input type="text" id="subCategory" />
						</FormGroup>
					</Col>
				</Row>
				<FormGroup>
					<Label for="description">Task Description</Label>
					<Input type="textarea" id="description" bind:value={userTask.taskDescription} />
				</FormGroup>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled on:click={toggle}>Add Task</Button>
		</ModalFooter>
	</Modal>
</div>
