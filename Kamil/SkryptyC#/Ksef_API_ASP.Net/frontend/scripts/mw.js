import 'dotenv/config';              
import dotenv from 'dotenv';
import fs from 'node:fs/promises';
import {dictionaryType, dictionaryFormat} from './dictionary.js';
import path from 'node:path'
import { resolve, dirname } from 'path'
import { fileURLToPath } from 'url'


const __dirname = dirname(fileURLToPath(import.meta.url))
dotenv.config({ path: resolve(__dirname, '../../.env') });

const projectPath = process.env.PROJCETPATH;

